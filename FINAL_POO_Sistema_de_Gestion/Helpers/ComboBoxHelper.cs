using System;

namespace FINAL_POO_Sistema_de_Gestion.Helpers
{
    public static class ComboBoxHelper
    {
        public static string ExtraerId(string comboBoxText)
        {
            if (string.IsNullOrEmpty(comboBoxText)) return null;
            int start = comboBoxText.IndexOf('[');
            int end = comboBoxText.IndexOf(']');
            if (start < 0 || end < 0 || end <= start) return null;
            return comboBoxText.Substring(start + 1, end - start - 1);
        }
    }
}
