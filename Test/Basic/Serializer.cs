using Dynamics.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace Test.Basic
{
    [TestClass]
    public class TestSerializer
    {
        private const string attribute = "attribute";
        private JObject json;
        private JObject expected;

        [TestInitialize]
        public void SetUp()
        {
            json = new JObject();
            expected = new JObject();
        }

        [TestMethod]
        public void add_integer_to_jobject()
        {
            // Arrange
            int _int = 1;

            // Act
            json.AddInteger(attribute, _int);

            // Assert
            expected[attribute] = _int;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_double_to_jobject()
        {
            // Arrange 
            double _double = 1.01;

            // Act
            json.AddDouble(attribute, _double);

            // Assert
            expected[attribute] = _double;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_bool_to_jobject()
        {
            // Arrange
            bool isTrue = true;

            // Act
            json.AddBool(attribute, isTrue);

            // Assert
            expected[attribute] = isTrue;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_bool_if_provided()
        {
            // Arrange
            bool isTrue = true;

            // Act
            json.AddIfProvided(attribute, isTrue);

            // Assert
            expected[attribute] = isTrue;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_string_to_jobject()
        {
            // Arrange
            string _string = "string";

            // Act
            json.AddString(attribute, _string);

            // Assert
            expected[attribute] = _string;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_if_provided_value_not_null()
        {
            // Arrange
            double? _double = 1.01;

            // Act
            json.AddIfProvided(attribute, _double);

            // Assert
            expected[attribute] = _double;
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void add_if_provided_value_null()
        {
            // Arrange
            double? nullDouble = null;

            // Act
            json.AddIfProvided(attribute, nullDouble);

            // Assert
            Assert.AreEqual(expected.ToString(), json.ToString());
        }

        [TestMethod]
        public void get_nullable_type()
        {
            // Arrange
            var type = typeof(double?);

            // Act 
            var result = Serializer.GetType(type);

            // Assert
            Assert.AreEqual(typeof(double), result);
        }

        [TestMethod]
        public void get_type()
        {
            // Arrange
            var type = typeof(double);

            // Act
            var result = Serializer.GetType(type);

            // Assert
            Assert.AreEqual(typeof(double), result);
        }
    }
}
