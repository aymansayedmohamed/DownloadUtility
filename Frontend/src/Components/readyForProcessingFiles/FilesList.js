import React, {PropTypes} from 'react';
import FilesListRow from './FilesListRow';

const FilesList = ({files}) => {
    return(
        <table className="table table-striped">
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th>Source</th>
                <th>Processing Status</th>
            </tr>
        </thead>
        <tbody>
            {
                files.map(file => 
                    <FilesListRow key = {file.Id} file ={file}/>
                    )
            }
        </tbody>
        </table>
    );

};

FilesList.propTypes = {
    files: PropTypes.array.isRequired
};

export default FilesList;