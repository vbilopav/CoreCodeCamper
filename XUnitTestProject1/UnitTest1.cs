using CoreCodeCamper.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            using (var ctx = new CodeCamperDataContext())
            {
                var result = ctx.TestModel.FromSql("select id, 'blah' as word from test_table where id = 1").FirstOrDefault();
            }
        }

        [Fact]
        public void Test2()
        {
            using (var ctx = new CodeCamperDataContext())
            {
                var result = ctx.Database.GetDbConnection().Query<TestModel>("select id, 'blah' as word from test_table where id = 1").FirstOrDefault();
            }
        }

        [Fact]
        public void Test3()
        {
            using (var ctx = new CodeCamperDataContext())
            {
                var result = ctx.TestModel.FromSql("select id, 'blah' as word from test_table").Where(t => t.Id == 1).FirstOrDefault();
            }
        }

        [Fact]
        public void Test4()
        {
            using (var ctx = new CodeCamperDataContext())
            {
                var query = ctx.TestModel.FromSql("select id, 'blah' as word from test_table").Where(t => t.Id == 1);

                var sql = query.ToString();
            }
        }
    }

}
