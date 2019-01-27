import * as types from '../actions/actionTypes';

export default function fileReducer(state = [],action){
    switch (action.type){
      
        case types.LOAD_FILES_SUCCESS:
            return action.files;

        case types.APPROVE_FILE_SUCCESS:
            return [
                ...state.filter(file => file.Id !== action.file.Id)
            ];

        case types.REJECT_FILE_SUCCESS:
            return [
                ...state.filter(file => file.Id !== action.file.Id)
            ];

        case types.DOWNLOAD_BATCH_FILES_SUCCESS:
        return [
                ...action.files
            ];

        default:
            return state;
    }
}