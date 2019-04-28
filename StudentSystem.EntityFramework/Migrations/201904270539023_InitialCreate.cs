namespace StudentSystem.EntityFramework.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    StudentNo = c.String(maxLength: 50),
                    ShouldScore = c.Int(nullable: false),
                    RealScore = c.Int(nullable: false),
                    Professional = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                    Users_Id = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Students_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                // .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    UserName = c.String(maxLength: 50),
                    Password = c.String(maxLength: 50),
                    Name = c.String(maxLength: 50),
                    Gender = c.Int(nullable: false),
                    Email = c.String(maxLength: 50),
                    Phone = c.String(maxLength: 50),
                    IdCard = c.String(maxLength: 50),
                    UserType = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Users_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StudentSelectCourse",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    StudentId = c.Long(nullable: false),
                    SelectCourseId = c.Long(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_StudentSelectCourse_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                // .ForeignKey("dbo.SelectCourse", t => t.SelectCourseId)
                //.ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.SelectCourseId);

            CreateTable(
                "dbo.SelectCourse",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CouresId = c.Long(nullable: false),
                    TeacherId = c.Long(nullable: false),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SelectCourse_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                //  .ForeignKey("dbo.Course", t => t.CouresId)
                // .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.CouresId)
                .Index(t => t.TeacherId);

            CreateTable(
                "dbo.Course",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CourseName = c.String(maxLength: 50),
                    Score = c.Int(nullable: false),
                    Duration = c.Int(nullable: false),
                    Professional = c.Int(nullable: false),
                    PersonLimit = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Course_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Teachers",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TeacherNo = c.String(maxLength: 50),
                    TeacherRank = c.Int(nullable: false),
                    Professional = c.Int(nullable: false),
                    CreationTime = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    ModifyTime = c.DateTime(),
                    Users_Id = c.Long(),
                },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Teachers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                //  .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.StudentSelectCourse", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentSelectCourse", "SelectCourseId", "dbo.SelectCourse");
            DropForeignKey("dbo.SelectCourse", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.SelectCourse", "CouresId", "dbo.Course");
            DropForeignKey("dbo.Students", "Users_Id", "dbo.Users");
            DropIndex("dbo.Teachers", new[] { "Users_Id" });
            DropIndex("dbo.SelectCourse", new[] { "TeacherId" });
            DropIndex("dbo.SelectCourse", new[] { "CouresId" });
            DropIndex("dbo.StudentSelectCourse", new[] { "SelectCourseId" });
            DropIndex("dbo.StudentSelectCourse", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "Users_Id" });
            DropTable("dbo.Teachers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Teachers_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Course",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Course_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SelectCourse",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SelectCourse_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.StudentSelectCourse",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_StudentSelectCourse_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Users",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Users_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Students",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Students_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
