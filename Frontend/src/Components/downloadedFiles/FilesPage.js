import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import * as fileActions from '../../actions/fileActions';
import {bindActionCreators} from 'redux';
import FilesList from './FilesList';

class FilesPage extends React.Component{

    constructor(props, context){
        super(props,context);
    }

    render(){
        //const {files} = this.props;
        return(
            <div>
                <h1>Downloaded Files</h1>
                <FilesList files={this.props.files}/>                
            </div>
        );
    }
}

FilesPage.propTypes = {
    actions: PropTypes.object.isRequired,
    files: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps){
    return {
        files : state.files
    };
}

function mapDispatchToProps(dispatch){
    return{
        actions : bindActionCreators(fileActions,dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(FilesPage);