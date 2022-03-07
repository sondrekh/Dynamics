using System;

namespace Dynamics.Basic
{
    public class BuildUrl
    {
        private const string _api = "api/data/v9.1";
        private string _resourceUrl;

        public BuildUrl(string resourceUrl)
        {
            _resourceUrl = resourceUrl;
        }

        public string GetRecord(IRequest r)
        {
            return $"{TargetRecord(r)}?{r.select}";
        }

        private string TargetRecord(IRequest r)
        {
            return $"{_resourceUrl}/{_api}/{r.entityName}({r.recordId})";
        }

        public string Post(IRequest r)
        {
            return $"{_resourceUrl}/{_api}/{r.entityName}";
        }

        public string GetList(IRequest r)
        {
            return $"{_resourceUrl}/{_api}/{r.entityName}?{r.select}{r.filter}{r.expand}";
        }

        public string Patch(IRequest request)
        {
            return TargetRecord(request);
        }

        public string Delete(IRequest request)
        {
            return TargetRecord(request);
        }

        public string Put(IRequest r)
        {
            return $"{TargetRecord(r)}/{r.fieldName}";
        }
    }

}
