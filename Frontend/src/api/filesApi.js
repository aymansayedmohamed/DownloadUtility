import axios from 'axios';

class FilesApi {

  static downloadFiles() {         
    return axios.post(`http://localhost:59346/api/Download/DownloadSources`
                      , 'ftp://speedtest.tele2.net/5MB.zip,https://www.everyarabstudent.com/img2017/top_more_retina.png'
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
    debugger;
     return axios.put(`http://localhost:59346/api/Download/ApproveFile`,file  );
  }

  static rejectFile(file) {
    debugger;
     return axios.put(`http://localhost:59346/api/Download/RejectFile`,file  );
  }
  

 
}

export default FilesApi;