import * as types from '../constants/actionTypes';
import fileApi from '../api/filesApi';

export function loadFilesSuccess(files){
    return{type: types.LOAD_FILES_SUCCESS, files};
}

export function approveFileSuccess(file){
    return {type: types.APPROVE_FILE_SUCCESS, file};
}

export function rejectFileSuccess(file){
    return {type: types.REJECT_FILE_SUCCESS, file};
}

export function downloadBatchFilesSuccess(files){
    return {type: types.DOWNLOAD_BATCH_FILES_SUCCESS, files};
}




export function downloadBatchFiles(batchSources){
    return function(dispatch){
        
        return fileApi.downloadBatchFiles(batchSources).then(Response =>{
            dispatch(downloadBatchFilesSuccess(Response.data));
        }).catch(error => {
            throw(error);
        });
    };
}

export function loadFiles(){
    return function(dispatch){
        
        return fileApi.getAllFiles().then(Response =>{
            dispatch(loadFilesSuccess(Response.data));
        }).catch(error => {
            throw(error);
        });
    };
}

export function approveFile(file){

    return function(dispatch,getState){

        return fileApi.approveFile(file).then(Response =>{
            dispatch(approveFileSuccess(Response.data));
        }).catch(error => {
            throw(error);
        });

    };
}

export function rejectFile(file){

    return function(dispatch,getState){

        return fileApi.rejectFile(file).then(Response => {
            dispatch(rejectFileSuccess(Response.data));
        }).catch(error => {
            throw(error);

        });
    };
}