using System.Data;

namespace CN100.EnterprisePlatform.ORM.DB
{
    public class LitNullConstraint : LitConstraint
    {
        public LitNullConstraint(string column, bool isNull)
            : base(column, isNull ? "is" : "is not", null)
        { }

        public override string GetSql(LitTable table, ref int offset)
        {
            string name = table.Translate(this.Column);
            if (name == null || name.Length == 0)
                throw new LightException(string.Format("column {0} not found in table {1}",
                                                       this.Column, table.Name));

            return string.Format("[{0}] {1} null", name, this.Operator);
        }

        public override void SetParameters(IDbCommand cmd)
        {
            // nothing to do
        }
    }
}
