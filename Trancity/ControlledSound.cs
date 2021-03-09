using Engine.Sound;

namespace Trancity
{
    /// <summary>
    /// Звук, который может контролироваться переменными
    /// </summary>
    public class ControlledSound
    {
        /// <summary>
        /// Система сравнения в стиле ОМСИ
        /// </summary>
        public enum ComparsionType
        {
            NotEqual = 0,
            Equal = 1,
            LessThan = 2,
            BiggerThan = 3,
            LessOrEqual = 4,
            BiggerOrEqual = 5
        }

        /// <summary>
        /// Переменная-условие для изменения звука
        /// </summary>
        public class Condition
        {
            /// <summary>
            /// Имя переменной, которую сверяют
            /// </summary>
            public string ConditionName;
            /// <summary>
            /// Значение для сверки
            /// </summary>
            public float IfValue;
            /// <summary>
            /// Тип сравнения
            /// </summary>
            public ComparsionType CompareType;
        }

        private ISound3D _Sound;
        private string _VariableName;
        private Condition[] _Conditions;

        public ISound3D Sound => _Sound;
        public string VariableName => _VariableName;

        /// <summary>
        /// Ищет в данном звуке наличие переменной для сверки
        /// </summary>
        /// <param name="condition">название переменной для поиска</param>
        /// <returns>Возвращает класс перемнной-условия, либо null если не найдено условие</returns>
        public Condition GetCondition(string condition)
        {
            if (_Conditions == null) return null;

            for (int i = 0; i < _Conditions.Length; i++)
            {
                if (_Conditions[i].ConditionName == condition)
                {
                    return _Conditions[i];
                }
            }

            return null;
        }

        public ControlledSound(string varname, ISound3D sound, params Condition[] conditions)
        {
            _Sound = sound;
            _VariableName = varname;
            _Conditions = conditions;
        }
    }
}