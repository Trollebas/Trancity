/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 25.06.2017
 * Time: 1:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Common;
using Engine;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Trancity
{
    partial class Editor
    {
        // TODO: avoid hardcode stack size & constructor call
        private RestrictedStack<EditorAction> _undoStack = new RestrictedStack<EditorAction>(5);
        private EditorAction _pendingAction;
        private bool _pendingActionApplied;

        /// <summary>
        /// Already DOES action and saves it into stack
        /// </summary>
        /// <param name="action"></param>
        private void DoRegisterAction(EditorAction action)
        {
            if (action == null)
                //throw new ArgumentNullException("action", "Действие редактора не может быть нулевым");
                MessageBox.Show("Действие редактора не может быть нулевым", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            action.Parent = this;
            _undoStack.Push(action);
            action.Do();
            return;
        }

        private void UndoAction()
        {
            if (_undoStack.Count == 0)
                //throw new ArgumentNullException("action", "Не удается отменить из-за пустого буфера");
                MessageBox.Show("Не удается отменить из-за пустого буфера", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //ExceptionHandlerForm = false;
            var action = _undoStack.Pop();
            action.Undo();

        }

        private void RegisterPendingAction(EditorAction action, bool doNow = false)
        {
            if (_pendingAction != null)
                //throw new InvalidOperationException("Ожидающее действие не является нулевым");
                MessageBox.Show("Ожидающее действие не является нулевым", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (action == null)
                //throw new ArgumentNullException("action", "Не удается отменить из-за пустого буфера");
                MessageBox.Show("Не удается отменить из-за пустого буфера", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            action.Parent = this;
            if (doNow)
                action.Do();
            _pendingAction = action;
            _pendingActionApplied = doNow;
        }

        private void DoPendingAction()
        {
            if (_pendingAction == null)
                //throw new InvalidOperationException("Ожидающее действие не является нулевым");
                MessageBox.Show("Ожидающее действие не является нулевым", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            _undoStack.Push(_pendingAction);
            if (!_pendingActionApplied)
                _pendingAction.Do();
            _pendingAction = null;
        }

        private void ClearPendingAction()
        {
            if (_pendingAction == null)
                //throw new InvalidOperationException("Ожидающее действие не является нулевым");
                MessageBox.Show("Ожидающее действие не является нулевым", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (_pendingActionApplied)
                _pendingAction.Undo();

            _pendingAction = null;
        }

        #region Action impl-s

        private class AddRoadsAction : EditorAction
        {
            private Road[] _roads;

            public AddRoadsAction(params Road[] roads)
            {
                _roads = roads;
            }

            #region EditorAction implementation

            public override void Do()
            {
                for (int i = 0; i < _roads.Length; i++)
                    _parent.мир.listДороги.Add(_roads[i]);
                foreach (var spline in _parent.мир.ВсеДороги)
                    spline.ОбновитьСледующиеДороги(_parent.мир.ВсеДороги);
                _parent.UpdateSplinesList();
            }

            public override void Undo()
            {
                for (int i = _roads.Length; i >= 0; i--)
                    _parent.мир.listДороги.Remove(_roads[i]);
                foreach (var spline in _parent.мир.ВсеДороги)
                    spline.ОбновитьСледующиеДороги(_parent.мир.ВсеДороги);
                _parent.UpdateSplinesList();
            }

            #endregion
        }

        private class RemoveRoadAction : EditorAction
        {
            private int _index;
            private Road _road;

            public RemoveRoadAction(Road road)
            {
                _road = road;
            }

            public RemoveRoadAction(int index)
            {
                _index = index;
            }

            #region EditorAction implementation

            public override void Do()
            {
                if (_road != null)
                    _index = _parent.мир.listДороги.IndexOf(_road);
                else
                    _road = (Road)_parent.мир.listДороги[_index];

                _parent.мир.listДороги.RemoveAt(_index);
                foreach (var spline in _parent.мир.ВсеДороги)
                    spline.ОбновитьСледующиеДороги(_parent.мир.ВсеДороги);
                _parent.UpdateSplinesList();
            }

            public override void Undo()
            {
                _parent.мир.listДороги.Insert(_index, _road);
                foreach (var spline in _parent.мир.ВсеДороги)
                    spline.ОбновитьСледующиеДороги(_parent.мир.ВсеДороги);
                _parent.UpdateSplinesList();
            }

            #endregion
        }

        private class AddWiresAction : EditorAction
        {
            private Контактный_провод[] _wires;

            public AddWiresAction(params Контактный_провод[] wires)
            {
                _wires = wires;
            }

            #region EditorAction implementation

            public override void Do()
            {
                var cityWires = new List<Контактный_провод>(_parent.мир.контактныеПровода);
                cityWires.AddRange(_wires);
                // TODO: recalc disabled status for newly added wires

                _parent.мир.контактныеПровода = cityWires.ToArray();
            }

            public override void Undo()
            {
                var cityWires = new List<Контактный_провод>(_parent.мир.контактныеПровода);
                foreach (var wire in _wires)
                {
                    cityWires.Remove(wire);
                }
                // TODO: recalc disabled status for whole world wires
                _parent.мир.контактныеПровода = cityWires.ToArray();
            }

            #endregion
        }

        private class AddTramWireAction : EditorAction
        {
            private Трамвайный_контактный_провод _wire;

            public AddTramWireAction(Трамвайный_контактный_провод wire)
            {
                _wire = wire;
            }

            #region EditorAction implementation

            public override void Do()
            {
                var cityWires = new List<Трамвайный_контактный_провод>(_parent.мир.контактныеПровода2);
                cityWires.Add(_wire);
                // TODO: recalc disabled status for newly added wires
                _parent.мир.контактныеПровода2 = cityWires.ToArray();
            }

            public override void Undo()
            {
                var cityWires = new List<Трамвайный_контактный_провод>(_parent.мир.контактныеПровода2);
                cityWires.Remove(_wire);
                // TODO: recalc disabled status for whole world wires
                _parent.мир.контактныеПровода2 = cityWires.ToArray();
            }

            #endregion
        }

        private class RemoveWiresAction : EditorAction
        {
            private Контактный_провод[] _wires;

            public RemoveWiresAction(params Контактный_провод[] wires)
            {
                _wires = wires;
            }

            #region EditorAction implementation

            public override void Do()
            {
                var cityWires = new List<Контактный_провод>(_parent.мир.контактныеПровода);
                foreach (var wire in _wires)
                {
                    cityWires.Remove(wire);
                }
                // TODO: recalc disabled status for whole world wires
                _parent.мир.контактныеПровода = cityWires.ToArray();
            }

            public override void Undo()
            {
                var cityWires = new List<Контактный_провод>(_parent.мир.контактныеПровода);
                cityWires.AddRange(_wires);
                // TODO: recalc disabled status for newly added wires
                _parent.мир.контактныеПровода = cityWires.ToArray();
            }

            #endregion
        }

        private class RemoveTramWireAction : EditorAction
        {
            private Трамвайный_контактный_провод _wire;

            public RemoveTramWireAction(Трамвайный_контактный_провод wire)
            {
                _wire = wire;
            }

            #region EditorAction implementation

            public override void Do()
            {
                var cityWires = new List<Трамвайный_контактный_провод>(_parent.мир.контактныеПровода2);
                cityWires.Remove(_wire);
                // TODO: recalc disabled status for whole world wires
                _parent.мир.контактныеПровода2 = cityWires.ToArray();
            }

            public override void Undo()
            {
                var cityWires = new List<Трамвайный_контактный_провод>(_parent.мир.контактныеПровода2);
                cityWires.Add(_wire);
                // TODO: recalc disabled status for newly added wires
                _parent.мир.контактныеПровода2 = cityWires.ToArray();
            }

            #endregion
        }

        private class AddStopAction : EditorAction
        {
            private Stop _stop;

            public AddStopAction(Stop stop)
            {
                _stop = stop;
            }

            #region EditorAction implementation

            public override void Do()
            {
                _parent.мир.остановки.Add(_stop);
                _parent.UpdateStopsList();

                // TODO: drop this shit
                _parent.Stops_Box.SelectedIndex = _parent.Stops_Box.Items.Count - 1;
            }

            public override void Undo()
            {
                var index = _parent.мир.остановки.IndexOf(_stop);
                if (index != (_parent.мир.остановки.Count - 1))
                    //throw new InvalidOperationException("Массив остановок не заканчивается добавлением остановки");
                    MessageBox.Show("Массив остановок не заканчивается добавлением остановки", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _parent.мир.остановки.RemoveAt(index);
                _parent.UpdateStopsList();
            }

            #endregion
        }

        private class MoveStopAction : EditorAction
        {
            private Stop _stop;
            private Road _initialSpline, _newSpline;
            private double _initialDist, _newDist;

            public MoveStopAction(Stop obj)
            {
                _stop = obj;
                _initialSpline = obj.road;
                _initialDist = obj.distance;
            }

            #region EditorAction implementation

            public override void Do()
            {
                if (_newSpline == null)
                {
                    _newSpline = _stop.road;
                    _newDist = _stop.distance;
                    return;
                }
                _stop.road = _newSpline;
                _stop.distance = _newDist;
                _stop.UpdatePosition(_parent.мир);
            }

            public override void Undo()
            {
                _stop.road = _initialSpline;
                _stop.distance = _initialDist;
                _stop.UpdatePosition(_parent.мир);
            }

            #endregion
        }

        private class RemoveStopAction : EditorAction
        {
            private int _index;
            private Stop _object;

            public RemoveStopAction(int index)
            {
                _index = index;
            }

            #region EditorAction implementation

            public override void Do()
            {
                _object = _parent.мир.остановки[_index];

                _parent.мир.остановки.RemoveAt(_index);
                _parent.UpdateStopsList();
            }

            public override void Undo()
            {
                _parent.мир.остановки.Insert(_index, _object);
                _parent.UpdateStopsList();
                //_parent.UpdateRouteControls(Route );
                // TODO: insert back to trips?
            }

            #endregion
        }

        private class AddDepotAction : EditorAction
        {
            private Парк _depot;

            public AddDepotAction(Парк depot)
            {
                _depot = depot;
            }

            #region EditorAction implementation

            public override void Do()
            {
                List<Парк> list = new List<Парк>(_parent.мир.парки);
                list.Add(_depot);
                _parent.мир.парки = list.ToArray();
                _parent.UpdateParksList();
                _parent.Park_Box.SelectedIndex = _parent.Park_Box.Items.Count - 1;
                // TODO: update trip depots another way
                _parent.Narad_Box_SelectedIndexChanged(null, null);
            }

            public override void Undo()
            {
                var index = _parent.Park_Box.SelectedIndex;
                List<Парк> list = new List<Парк>(_parent.мир.парки);
                list.Remove(_depot);
                _parent.мир.парки = list.ToArray();
                _parent.UpdateParksList();
                if (index >= list.Count)
                    index--;
                _parent.Park_Box.SelectedIndex = index;
                // TODO: update trip depots another way
                _parent.Narad_Box_SelectedIndexChanged(null, null);
            }

            #endregion
        }

        private class RemoveDepotAction : EditorAction
        {
            private int _index;
            private Парк _depot;

            public RemoveDepotAction(int index)
            {
                _index = index;
            }

            #region EditorAction implementation

            public override void Do()
            {
                List<Парк> list = new List<Парк>(_parent.мир.парки);
                if (_depot == null)
                    _depot = list[_index];
                list.RemoveAt(_index);
                _parent.мир.парки = list.ToArray();
                _parent.UpdateParksList();
                _parent.Park_Box.SelectedIndex = _index - 1;
                // TODO: update trip depots another way
                _parent.Narad_Box_SelectedIndexChanged(null, null);
            }

            public override void Undo()
            {
                List<Парк> list = new List<Парк>(_parent.мир.парки);
                list.Insert(_index, _depot);
                _parent.мир.парки = list.ToArray();
                _parent.UpdateParksList();
                _parent.Park_Box.SelectedIndex = _index;
                // TODO: update trip depots another way
                _parent.Narad_Box_SelectedIndexChanged(null, null);
            }

            #endregion
        }

        private class AddObjectAction : EditorAction
        {
            private Объект _object;

            public AddObjectAction(Объект obj)
            {
                _object = obj;
            }

            #region EditorAction implementation

            public override void Do()
            {
                _parent.мир.объекты.Add(_object);
                _parent.UpdateObjectsList();

                // TODO: drop this shit
                _parent.Objects_Instance_Box.SelectedIndex = _parent.Objects_Instance_Box.Items.Count - 1;
            }

            public override void Undo()
            {
                var index = _parent.мир.объекты.IndexOf(_object);
                if (index != (_parent.мир.объекты.Count - 1))
                    //throw new InvalidOperationException("Массив объектов не заканчивается добавленным объектом");
                    MessageBox.Show("Массив объектов не заканчивается добавленным объектом", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _parent.мир.объекты.RemoveAt(index);
                _parent.UpdateObjectsList();
            }

            #endregion
        }

        private class MoveObjectAction : EditorAction
        {
            private Объект _object;
            private Double3DPoint _initialPos, _newPos;
            private double _initialAngle, _newAngle;
            private bool _isDone;

            public MoveObjectAction(Объект obj)
            {
                _object = obj;
                _initialPos = obj.Position3D;
                _initialAngle = obj.direction;
            }

            #region EditorAction implementation

            public override void Do()
            {
                if (_isDone)
                {
                    _object.Position3D = _newPos;
                    _object.angle0 = _newAngle;
                    return;
                }
                _newPos = _object.Position3D;
                _newAngle = _object.direction;
                _isDone = true;
            }

            public override void Undo()
            {
                _object.Position3D = _initialPos;
                _object.angle0 = _initialAngle;
            }

            #endregion
        }

        private class RemoveObjectAction : EditorAction
        {
            private int _index;
            private Объект _object;

            public RemoveObjectAction(int index)
            {
                _index = index;
            }

            #region EditorAction implementation

            public override void Do()
            {
                _object = _parent.мир.объекты[_index];

                _parent.мир.объекты.RemoveAt(_index);
                _parent.UpdateObjectsList();
            }

            public override void Undo()
            {
                _parent.мир.объекты.Insert(_index, _object);
                _parent.UpdateObjectsList();
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// EditorAction describes abstract undo- and redoable action.
        /// </summary>
        private abstract class EditorAction
        {
            protected Editor _parent;

            public Editor Parent
            {
                set
                {
                    _parent = value;
                }
            }

            /// <summary>
            /// (Re)Apply action.
            /// </summary>
            public abstract void Do();

            /// <summary>
            /// Undo previously applied action.
            /// </summary>
            public abstract void Undo();
        }
    }
}
