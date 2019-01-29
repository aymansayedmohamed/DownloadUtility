import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import FileViewer from './FileViewer';
import * as fileTypes from '../../constants/fileTypes';

function setup(src,fileType){
    let props ={
        src:src ,
        fileType: fileType};

    return shallow(<FileViewer {...props}/>);

}

describe('FileViewer component tests', () => {

it('FileViewer renders ViewImage component if type is IMAGE_FILE_TYPE',() => {
    const wrapper = setup('',fileTypes.IMAGE_FILE_TYPE);
    expect(wrapper.find('ViewImage').length).toBe(1);
});

it('FileViewer renders ViewWebPage component if type is WEBPAGE_FILE_TYPE',() => {
    const wrapper = setup('','WEBPAGE_FILE_TYPE');
    expect(wrapper.find('ViewWebPage').length).toBe(1);
});

it('FileViewer renders UnsuportedFileFormat component if type is not supported',() => {
    const wrapper = setup('',"anyText");
    expect(wrapper.find('UnsuportedFileFormat').length).toBe(1);

});

});








