import React , {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import DownLoadBatchForm from './downLoadBatchForm';
import * as fileActions from '../../actions/fileActions';

export class DownloadBatchFilesPage extends React.Component{

    constructor(props, context){
        super(props, context);
        
        this.state={
            batchSources : Object.assign({}, this.props.batchSources),
            errors:{}
            };
        this.updateSourcesState = this.updateSourcesState.bind(this);
        this.downloadBatchFiles = this.downloadBatchFiles.bind(this);
    }

    updateSourcesState(event){
        const field = event.target.name;
        let batchSources = this.state.batchSources;
        batchSources[field] = event.target.value;
        return this.setState({batchSources: batchSources});
    }

    downLoadBatchFormValid(){
        let formIsValid = true;
        let errors = {};

        if(this.state.batchSources.Sources && this.state.batchSources.Sources.length<1){
            errors.title = "Sources is required";
            formIsValid = false;
        }

        this.setState({errors: errors});
        return formIsValid;
    }

    downloadBatchFiles(event){
        event.preventDefault();

        if(!this.downLoadBatchFormValid()){
            return;
        }

        this.props.actions.downloadBatchFiles(this.state.batchSources);
        this.context.router.push('/readyForProcessingFiles');
    }

    render(){
         return(
             <div>
                <h1>Download Batch Files</h1>
                <DownLoadBatchForm
                onDownload={this.downloadBatchFiles}
                onChange={this.updateSourcesState}
                batchSources={this.state.batchSources}
                errors={this.state.errors}
                />
             </div>
         );
    }

}

//Pull in the React Router context so router is available on this.context.router
DownloadBatchFilesPage.contextTypes ={
    router: PropTypes.object
};

DownloadBatchFilesPage.propTypes ={
    batchSources: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired
    };

function mapStateToProps(state , ownProps){
    
    let batchSources = {Sources:""};
    return{
        batchSources: batchSources
    };
}
function mapDispatchToProps(dispatch){
    return{
    actions : bindActionCreators(fileActions, dispatch)
    };
}
export default connect(mapStateToProps,mapDispatchToProps)(DownloadBatchFilesPage);