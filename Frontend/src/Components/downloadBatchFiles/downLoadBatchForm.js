import React, {PropTypes} from 'react';

const DownLoadBatchForm = ({batchSources, onChange, onDownload, loading, errors})=>{

    return(
        <form>
            <div className="form-group">
                <label htmlFor="Sources">Comment:</label>
                <textarea className="form-control" 
                    rows="5" 
                    id="Sources"
                    value={batchSources.Sources}
                    onChange={onChange}
                    errors={errors}
                    name="Sources"
                    >
                </textarea>
                {errors && <div className="alert alert-danger">{errors.title}</div>}
            </div>
            <div className="form-group">
                <input
                    type="submit"
                    disabled={loading}
                    value={loading? 'Downloading...' : 'Downloaded'}
                    className="btn btn-primary"
                    onClick={onDownload}/>
            </div>
        </form>

    );
};

DownLoadBatchForm.propTypes = {
    batchSources: PropTypes.object.isRequired,
    onChange: PropTypes.func.isRequired,
    onDownload: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    errors: PropTypes.object
};

export default DownLoadBatchForm;

