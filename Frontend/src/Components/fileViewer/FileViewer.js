import React, {PropTypes} from 'react';
import * as fileTypes from '../../constants/fileTypes';
import ViewImage from '../fileViewer/ViewImage';
import ViewWebPage from '../fileViewer/ViewWebPage';
import UnsuportedFileFormat from '../fileViewer/UnsuportedFileFormat';

const FileViewer = ({src, fileType})=>{
debugger;
        if(fileType == fileTypes.IMAGE_FILE_TYPE){
            return(
                <ViewImage
                src = {src}
                />
            );
        }

        else if(fileType == fileTypes.WEBPAGE_FILE_TYPE){
            return (
                <ViewWebPage
                src = {src}
                />
            );
        }

        return (
            <UnsuportedFileFormat/>
        );

        
        };


FileViewer.propTypes = {
    src: PropTypes.string.isRequired,
    fileType: PropTypes.string.isRequired
};

export default FileViewer;

