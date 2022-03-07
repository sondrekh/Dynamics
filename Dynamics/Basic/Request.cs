using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.Basic
{
    public class Request : IRequest
    {
        public string entityName { get; set; }
        public string select { get; set; }
        public string filter { get; set; }
        public string expand { get; set; }
        public string fieldName { get; set; }
        public string recordId { get; set; }
        public string body { get; set; }

        public Request Body(string body)
        {
            this.body = body;
            return this;
        }

        public Request RecordId(string recordId)
        {
            this.recordId = recordId;
            return this;
        }

        public Request EntityName(string entityName)
        {
            this.entityName = entityName;
            return this;
        }

        public Request Select(string select)
        {
            this.select = select;
            return this;
        }

        public Request Filter(string filter)
        {
            this.filter = filter;
            return this;
        }

        public Request Expand(string expand)
        {
            this.expand = expand;
            return this;
        }

        public Request FieldName(string fieldName)
        {
            this.fieldName = fieldName;
            return this;
        }
    }
}
