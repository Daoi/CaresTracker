using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class CTextWriter : DataSupport, IData
    {
        public CTextWriter(string cText)
        {
            CommandText = cText;
            CommandType = CommandType.Text;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}