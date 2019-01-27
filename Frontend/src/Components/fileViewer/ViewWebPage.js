import React, {PropTypes} from 'react';

const ViewWebPage = ({src})=>{

    return(

            <div className="form-group">
                <iframe  src={src}/>
            </div>

    );
};

ViewWebPage.propTypes = {
    src: PropTypes.string.isRequired
};

export default ViewWebPage;

