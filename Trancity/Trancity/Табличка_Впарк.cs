/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 22.10.2011
 * Время: 10:56
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Trancity
{
	using System;
	using Common;
//	using Microsoft.DirectX;
	using SlimDX;
	
	public class ТабличкаВПарк : MeshObject, MeshObject.IFromFile, IMatrixObject
    {
    	public Matrix matrix;
        private Transport _транспорт;

        public ТабличкаВПарк(Transport транспорт)
        {
        	_транспорт = транспорт;
        }

        public Matrix GetMatrix(int index)
        {
        	return matrix;
        }

        public string Filename
        {
        	get
            {
            	base.meshDir = _транспорт.модель.dir;
                return _транспорт.модель.табличка.filename;
            }
        }

        public int MatricesCount
        {
            get
            {
                return _транспорт.в_парк ? 1 : 0;
            }
        }
	}
}
