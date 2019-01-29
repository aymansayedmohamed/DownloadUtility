import React, {PropTypes} from 'react';

const UnsuportedFileFormat = ({src})=>{

    return(

            <div className="form-group">
                <h1>Unsuported file format</h1>
            </div>

    );
};

UnsuportedFileFormat.propTypes = {
    src: PropTypes.string.isRequired
};

export default UnsuportedFileFormat;

