using OfficeOpenXml;

namespace ConsultorioSeguros.Helpers
{
    public static class ExcelHelper
    {
        public static int GetFirstRowPosition(ExcelWorksheet worksheet, int sum)
        {
            return worksheet.Dimension.Start.Row + sum;
        }

        public static int GetLastRow(ExcelWorksheet worksheet)
        {
            return worksheet.Dimension.End.Row;
        }
    }
}