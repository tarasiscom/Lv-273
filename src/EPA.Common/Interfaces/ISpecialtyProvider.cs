using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    ///  This interface describes methods for getting specialty related data
    /// </summary>
    public interface ISpecialtyProvider
    {
        /// <summary>
        ///  This method retrieves collection of all subjects
        /// </summary>
        /// <returns>Collection of subjects</returns>
        IEnumerable<Subject> GetAllSubjects();

        /// <summary>
        ///  This method retrieves collection of all districts
        /// </summary>
        /// <returns>Collection of districts</returns>
        IEnumerable<District> GetAllDistricts();

        /// <summary>
        /// This method retrieves collection of general directions
        /// </summary>
        /// <returns> Collection of general directions </returns>
        IEnumerable<GeneralDirection> GetGeneralDirections();

        /// <summary>
        ///  This method retrieves list of specialties based on subjects
        /// </summary>
        /// <param name="userId"> User's Id </param>
        /// <param name="listSubjects"> List of chosen subjects </param>
        /// <param name="idDistrict"> Chosen district </param>
        /// <param name="page"> Page iterator</param>
        /// <returns> Collection of Specialties </returns>
        IEnumerable<Specialty> GetSpecialtyBySubjects(string userId, List<int> listSubjects, int idDistrict, int page);

        /// <summary>
        /// This method retrieves list of specialties based on general direction
        /// </summary>
        /// <param name="userId"> User's Id </param>
        /// <param name="idDirection"> Chosen general direction</param>
        /// <param name="idDistrict"> Chosen district </param>
        /// <param name="page"> Page iterator</param>
        /// <returns> Collection of Specialties </returns>
        IEnumerable<Specialty> GetSpecialtiesByDirection(string userId, int idDirection, int idDistrict, int page);

        /// <summary>
        /// This method retrieves count of pages for specialties chosed by direction
        /// </summary>
        /// <param name="directionId"> Chosen general direction</param>
        /// <param name="districtId"> Chosen district </param>
        /// <returns>Count of pages</returns>
        Count GetCountByDirection(int directionId, int districtId);

        /// <summary>
        /// This method retrieves count of pages for specialties chosed by subjects
        /// </summary>
        /// <param name="listSubjects"> List of chosen subjects</param>
        /// <param name="idDistrict"> Chosen district </param>
        /// <returns>Count of pages</returns>
        Count GetCountBySubjects(List<int> listSubjects, int idDistrict);

        /// <summary>
        /// This method retrieves specialties for chosen university and direction
        /// </summary>
        /// <param name="universityId">Chosen university</param>
        /// <param name="directionId">Chosen direction</param>
        /// <returns>Collection of specialties</returns>
        IEnumerable<Specialty> GetSpecialtiesInUniversity(int universityId, int directionId);
    }
}
