import React from 'react';
import FileViewer from '../fileViewer/FileViewer';

const FileForm = ({file, onApprove, onReject, loading, errors})=>{

    return(
        <form>
           <div className="row">

                <div className="col-md-12">
                    <div className="form-group">
                        <FileViewer
                            src = {file.Url}
                            fileType = {file.FileType}
                            />
                    </div>
                </div>
                <div className="col-md-12">
                    <div className="btn-toolbar" role="toolbar">
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
                        className="btn btn-danger"
                        onClick={onReject}
                        />
                    </div>
                </div>
           
               
            </div>

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

