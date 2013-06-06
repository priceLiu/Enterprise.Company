using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CN100.EnterprisePlatform.ORM.DB
{
    public class LitQuery : IQuery
    {
        protected List<LitConstraint> constraints;
        protected List<string> operators;
        protected List<LitOrder> orders;
        private string pending;

        public LitQuery()
        {
            constraints = new List<LitConstraint>(3);
            operators = new List<string>(2);
            orders = new List<LitOrder>(1);
        }

        public IConstraint Get(int index)
        {
            if (index > -1 && index < constraints.Count)
                return constraints[index];
            return null;
        }

        private void ErrorIfPending()
        {
            if (pending != null)
                throw new InvalidOperationException("you must finish operation started by Constrain(string)");
        }

        private void ErrorIfNotPending()
        {
            if (pending == null)
                throw new InvalidOperationException("you must call Constrain(string) first");
        }

        private void ErrorIfNull(object val)
        {
            if (val == null)
                throw new ArgumentNullException("parameter: val");
        }

        public IQuery Constrain(IQuery query)
        {
            ErrorIfPending();
            constraints.Add(new LitConstraint(query));
            return this;
        }

        public IQuery Constrain(string column)
        {
            ErrorIfPending();
            pending = column;
            return this;
        }

        public IQuery And()
        {
            ErrorIfPending();
            operators.Add("and");
            return this;
        }

        public IQuery Or()
        {
            ErrorIfPending();
            operators.Add("or");
            return this;
        }

        public IQuery In(IList values)
        {
            LitConstraint c = new LitInConstraint(pending, "in", values);
            AddConstraint(c);
            return this;
        }

        public IQuery NotIn(IList values)
        {
            LitConstraint c = new LitConstraint(pending, "not in", values);
            AddConstraint(c);
            return this;
        }

        protected void AddConstraint(string op, object val)
        {
            LitConstraint c = new LitConstraint(pending, op, val);
            AddConstraint(c);
        }

        protected void AddConstraint(LitConstraint c)
        {
            ErrorIfNotPending();
            constraints.Add(c);
            pending = null;
        }

        public IQuery Equal(object val)
        {
            if (val == null || val == DBNull.Value)
                AddConstraint(new LitNullConstraint(pending, true));
            else
                AddConstraint("=", val);
            return this;
        }

        public IQuery NotEqual(object val)
        {
            if (val == null || val == DBNull.Value)
                AddConstraint(new LitNullConstraint(pending, false));
            else
                AddConstraint("!=", val);
            return this;
        }

        public IQuery Greater(object val)
        {
            AddConstraint(">", val);
            return this;
        }

        public IQuery GreaterEqual(object val)
        {
            ErrorIfNull(val);
            AddConstraint(">=", val);
            return this;
        }

        public IQuery Less(object val)
        {
            ErrorIfNull(val);
            AddConstraint("<", val);
            return this;
        }

        public IQuery LessEqual(object val)
        {
            ErrorIfNull(val);
            AddConstraint("<=", val);
            return this;
        }

        public IQuery Like(string val)
        {
            ErrorIfNull(val);
            AddConstraint(" like ", val);
            return this;
        }

        public IQuery Order(string column, bool asc)
        {
            LitOrder o = new LitOrder(column, asc);
            orders.Add(o);
            return this;
        }

        protected bool IsComplete
        {
            get
            {
                int c = constraints.Count;
                int o = operators.Count;
                return (pending == null) && (((c == 0 || c == 1) && o == 0) || ((c - 1) == o));
            }
        }

        public virtual string GetSql(LitTable table, ref int offset)
        {
            if (!IsComplete)
                throw new InvalidOperationException("invalid query");

            StringBuilder buf = new StringBuilder();
            int sz = constraints.Count;
            if (sz > 0)
                buf.Append("where");
            for (int i = 0; i < sz; i++)
            {
                if (i > 0)
                {
                    string op = operators[i - 1];
                    buf.Append(" ").Append(op);
                }
                LitConstraint constraint = constraints[i];
                string sql = constraint.GetSql(table, ref offset);
                buf.Append(" ").Append(sql);
            }
            sz = orders.Count;
            if (sz > 0)
            {
                buf.Append(" order by ");
                for (int i = 0; i < sz; i++)
                {
                    if (i > 0)
                        buf.Append(",");
                    LitOrder order = orders[i];
                    string sql = order.GetSql(table);
                    buf.Append(sql);
                }
            }
            return buf.ToString();
        }

        public virtual void SetParameters(IDbCommand cmd)
        {
            foreach (LitConstraint constraint in constraints)
                constraint.SetParameters(cmd);
        }

        public virtual string GetSql(LitTable table)
        {
            int offset = 1;
            return GetSql(table, ref offset);
        }
    }
}
