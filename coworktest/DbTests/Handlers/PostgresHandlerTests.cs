using System;
using coworkpersistence.Handlers;
using Npgsql.TypeHandling;
using NUnit.Framework;

namespace coworktest.DbTests.Handlers {

    [TestFixture]
    public class PostgresHandlerTests {

        private PostgresHandler handler;
        
        [TearDown]
        public void TearDown() {
            
        }


        [SetUp]
        public void Setup() {
            handler = new PostgresHandler("Host=localhost;Database=cowork;Username=postgres;Password=ariba1");
        }


        [Test]
        public void ShouldThrowExceptionOnEmptyString() {
            var ex =Assert.Throws<Exception>(() => { 
                var pgHandler = new PostgresHandler("");
            });
            Assert.AreEqual("Connection String is Empty", ex.Message);
        }


        [Test]
        public void ShouldThrowWhenTryingToGetValueFromNullReader() {
            var ex =Assert.Throws<Exception>(() => { handler.GetValue<int>(0); });
            Assert.AreEqual("DbReader non initialisé", ex.Message);
        }
        
        
        [Test]
        public void ShouldThrowWhenTryingToReadFromNullReader() {
            var ex =Assert.Throws<Exception>(() => { handler.Read(); });
            Assert.AreEqual("DbReader non initialisé", ex.Message);
        }
    }

}