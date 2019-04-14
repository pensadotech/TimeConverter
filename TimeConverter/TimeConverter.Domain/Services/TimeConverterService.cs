using System;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;

namespace TimeConverter.Domain.Services
{
    // The service will be used by the client(e.g.Frontend), injecting the repository needed for the job
    // This place represents the seam that will connect the domain with specific implemenation that will bring data
    // into the applicaiton. 
    // It may seem redundant as repository implement simialr functions, but teh dependnecy injection 
    // provide freedom to implement the repository in different ways.

    public class TimeConverterService : ITimeConverterService
    {
        // Private members .................................
        private readonly ITimeConverterRepository _timeConverterRepository;

        // Constructors ....................................
        public TimeConverterService(ITimeConverterRepository repository)
        {
            if (repository == null)
            {
                // The service will receive the repository using constructor injection 
                // The repo must follow the IUserConfigRepository contract
                // If the value is null, then send back exception
                if (repository == null)
                {
                    throw new ArgumentNullException(nameof(repository));
                }
            }

            // Assign repositor for local use
            _timeConverterRepository = repository;
        }

        // Methods .....................................................

        // Implement functionality using the local repository, which obeys 
        // the IUserConfigRepository interface

        public DateTime ConvertSecondsToCurrentDateTime(double secSinceMidnight)
        {
            return _timeConverterRepository.ConvertSecondsToCurrentDateTime(secSinceMidnight);
        }

        public double ConvertString24HrTimeToSeconds(string targetTime24)
        {
            return _timeConverterRepository.ConvertString24HrTimeToSeconds(targetTime24);
        }

        public double ConvertDate12HrTimeToSeconds(DateTime targetDatetime12)
        {
            return _timeConverterRepository.ConvertDate12HrTimeToSeconds(targetDatetime12);
        }

    }
}
