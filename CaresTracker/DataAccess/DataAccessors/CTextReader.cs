using System.Data;

namespace CaresTracker.DataAccess.DataAccessors
{
    public class CTextReader : DataSupport, IData
    {
        public CTextReader(string cText)
        {
            CommandText = cText;
            CommandType = CommandType.Text;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}