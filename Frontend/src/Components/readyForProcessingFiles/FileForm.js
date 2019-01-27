import React from 'react';

const FileForm = ({file, onApprove, onReject, loading, errors})=>{

const type = 'png';
    return(
        <form>
           <h1>Id : {file.Id}</h1> 
           <h1>Source : {file.Source}</h1> 
           <h1>Processing Status : {file.ProcessingStatus}</h1> 

            <input
            type="submit"
            disabled={loading}
            value={loading? 'Approving...' : 'Approve'}
            className="btn btn-primary"
            onClick={onApprove}
            />

            <input
            type="submit"
            disabled={loading}
            value={loading? 'Rejecting...' : 'Reject'}
            className="btn btn-primary"
            onClick={onReject}
            />
        </form>
    );
};

FileForm.propTypes = {
    file: React.PropTypes.object.isRequired,
    onApprove: React.PropTypes.func.isRequired,
    onReject: React.PropTypes.func.isRequired,
    loading: React.PropTypes.bool,
    errors: React.PropTypes.object
};

export default FileForm;

