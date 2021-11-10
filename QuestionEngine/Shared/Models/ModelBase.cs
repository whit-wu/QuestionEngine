using QuestionEngine.Shared.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionEngine.Shared.Models
{
    public class ModelBase
    {
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }

        // the code below is a copy pasta of a project from this tutorial
        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/web-development-full-stack-csharp-with-blazor

        private Dictionary<String, Dictionary<String, String>> _errors = new Dictionary<string, Dictionary<string, string>>();

        public String GetValue(String fieldName)
        {
            var propertyInfo = this.GetType().GetProperty(fieldName);
            var value = propertyInfo.GetValue(this);

            if (value != null) { return value.ToString(); }
            return String.Empty;
        }
        public void SetValue(String fieldName, object value)
        {
            var propertyInfo = this.GetType().GetProperty(fieldName);
            propertyInfo.SetValue(this, value);
            CheckRules(fieldName);
        }

        
        // find a way to call this after error has been removed
        public String Errors(String fieldName)
        {
            if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName, new Dictionary<string, string>()); }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var value in _errors[fieldName].Values)
                sb.AppendLine(value);

            return sb.ToString();
        }

        public bool HasErrors(String fieldName)
        {
            if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName, new Dictionary<string, string>()); }
            return (_errors[fieldName].Values.Count > 0);
        }

        public bool CheckRules()
        {
            foreach (var propInfo in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                CheckRules(propInfo.Name);

            return HasErrors();
        }

        public void CheckRules(String fieldName)
        {
            var propertyInfo = this.GetType().GetProperty(fieldName);
            var attrInfos = propertyInfo.GetCustomAttributes(true);
            foreach (var attrInfo in attrInfos)
            {
                if (attrInfo is IModelRule modelrule)
                {
                    var value = propertyInfo.GetValue(this);
                    var result = modelrule.Validate(fieldName, value);
                    if (result.IsValid)
                    {
                        RemoveError(fieldName, attrInfo.GetType().Name);
                    }
                    else
                    {
                        AddError(fieldName, attrInfo.GetType().Name, result.Message);
                    }
                }
            }

        }

        private void AddError(String fieldName, String ruleName, String errorText)
        {
            if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName, new Dictionary<string, string>()); }
            if (_errors[fieldName].ContainsKey(ruleName)) { _errors[fieldName].Remove(ruleName); }
            _errors[fieldName].Add(ruleName, errorText);
            OnModelChanged();
        }

        private void RemoveError(String fieldName, String ruleName)
        {
            if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName, new Dictionary<string, string>()); }
            if (_errors[fieldName].ContainsKey(ruleName))
            {
                _errors[fieldName].Remove(ruleName);
                OnModelChanged();
            }
        }

        public bool HasErrors()
        {
            int errorcount = 0;
            foreach (var key in _errors.Keys)
                errorcount += _errors[key].Keys.Count;
            return (errorcount != 0);
        }

        public event EventHandler<EventArgs> ModelChanged;

        protected void OnModelChanged()
        {
            ModelChanged?.Invoke(this, new EventArgs());
        }
    }
}
