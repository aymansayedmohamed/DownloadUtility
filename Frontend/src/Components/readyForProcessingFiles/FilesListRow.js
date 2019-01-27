import React, {PropTypes} from 'react';
import {Link} from 'react-router';

const FilesListRow = ({file}) => {

    return (
        <tr>
            <td><Link to={'/file/' + file.Id}>View</Link></td>
            <td>{file.Source}</td>
            <td>{file.ProcessingStatus}</td>
        </tr>
    );
};

FilesListRow.propTypes = {
    file : PropTypes.object.isRequired 
};

export default FilesListRow;
