using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CrimeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "/crimeCity?city={city}&state={state}")]
        int crimeCity(string city, string state);

        [OperationContract]
        [WebGet(UriTemplate = "/crimeZip?zipcode={zipcode}")]
        int crimeZip(int zipcode);

        // TODO: Add your service operations here
    }


    
}
