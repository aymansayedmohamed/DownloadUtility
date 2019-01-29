import React, {PropTypes} from 'react';

const ViewText = ({src})=>{

    return(

            <div className="form-group">
                <div>
                    {}
                </div>
            </div>

    );

    
    function readFile(src) {

        const fileReader = new FileReader(); 
        fileReader.onload = function (e) { 
        const fileContents = document.getElementById('filecontents'); 
        fileContents.innerText = fileReader.result; 
        } 
        fileReader.readAsText(src);
    
    }

};



ViewText.propTypes = {
    src: PropTypes.string.isRequired
};

export default ViewText;

