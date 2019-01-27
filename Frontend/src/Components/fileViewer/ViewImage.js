import React, {PropTypes} from 'react';

const ViewImage = ({src})=>{

    return(

            <div className="form-group">
                <img  src={src}/>
            </div>

    );
};

ViewImage.propTypes = {
    src: PropTypes.string.isRequired
};

export default ViewImage;

