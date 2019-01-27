import React , {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as fileActions from '../../actions/fileActions';
import FileForm from './FileForm';

class ManageFilePage extends React.Component{

    constructor(props, context){
        super(props, context);

        this.state = {
            errors: {}
        };

        this.approveFile = this.approveFile.bind(this);
        this.rejectFile = this.rejectFile.bind(this);
        
    }

    componentWillReceiveProps(nextProps){
            this.setState({file: Object.assign({},nextProps.file)});
    }

    approveFile(event){
        event.preventDefault();
        this.props.actions.approveFile(this.props.file);
        this.context.router.push('/readyForProcessingFiles');
    }

    rejectFile(event){
        event.preventDefault();
        this.props.actions.rejectFile(this.props.file);
        this.context.router.push('/readyForProcessingFiles');
    }

    render(){
        return(
            <FileForm 
            file={this.props.file}
            errors={this.state.errors}
            onApprove={this.approveFile}
            onReject={this.rejectFile}
            />
        );
    }
}

ManageFilePage.propTypes ={
    file: PropTypes.object.isRequired,
actions: PropTypes.object.isRequired
};

//Pull in the React Router context so router is available on this.context.router
ManageFilePage.contextTypes ={
    router: PropTypes.object
};

function getFileById(files,id){
    const file = files.filter(file => file.Id == id);
    
    if(file) 
         return file[0];

    return null;
}

function mapStateToProps(state , ownProps){

    const fileId = ownProps.params.Id; //from the path 'file/:Id'
    
    let file ={Id:'', Source: '',ProcessingStatus: ''};

    if(fileId && state.files.length > 0){
        file = getFileById(state.files, fileId);
    }

    return{
        file: file
    };
}

function mapDispatchToProps(dispatch){
    return{
        actions : bindActionCreators(fileActions, dispatch)
    };
} 

export default connect(mapStateToProps,mapDispatchToProps)(ManageFilePage);

