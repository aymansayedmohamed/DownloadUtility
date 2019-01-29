import axios from 'axios';

class FilesApi {

  static downloadBatchFiles(batchSources) {         
    return axios.post(`http://localhost:59346/api/Download/DownloadBatchFiles`
                      , batchSources
                      ,
                      {
                        headers: {
                                'Accept': 'application/json'                }
                      }
                    );
  }

  static getAllFiles() {
    return axios.get(`http://localhost:59346/api/Download/GetReadyForProcessingFiles`  );
   }

  static approveFile(file) {
    
     return axios.put(`http://localhost:59346/api/Download/ApproveFile`,file  );
  }

  static rejectFile(file) {
    
     return axios.put(`http://localhost:59346/api/Download/RejectFile`,file  );
  }
  

 
}

export default FilesApi;