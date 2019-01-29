import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import UnsuportedFileFormat from './UnsuportedFileFormat';

function setup(src){
    let props ={
        src: src
    };

    return shallow(<UnsuportedFileFormat {...props}/>);

}

describe('UnsuportedFileFormat component tests', () => {

it('UnsuportedFileFormat renders valid message',() => {
    const wrapper = setup("");
    expect(wrapper.find('h1').get(0).props.children).toEqual('Unsuported file format');

});

});








