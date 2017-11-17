using AutoMapper;
using EPA.Common.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class TestDetailedInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ApproximateTime { get; set; }

        public int QuestionsCount { get; set; }

        public List<Question> Questions { get; set; }

        public TestInfo ToCommon()
        {
            return Mapper.Map<TestInfo>(this);
        }
    }
}
