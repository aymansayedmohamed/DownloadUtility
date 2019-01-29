import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import FilesList from './FilesList';

function setup(files){
    let props ={
        files: files
    };

    return shallow(<FilesList {...props}/>);

}

describe('FilesList component tests', () => {

it('FilesList renders table',() => {
    const wrapper = setup([]);
    expect(wrapper.find('table').length).toBe(1);
});


it('FilesList renders number of row equal to the files count',() => {
    const wrapper = setup([{},{}]);
    expect(wrapper.find('FilesListRow').length).toBe(2);

});

});








