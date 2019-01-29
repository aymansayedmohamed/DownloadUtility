import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import FileForm from './FileForm';

function setup(file){
    let props ={
        file: file,
        onApprove: ()=>{},
        onReject: ()=>{},
        loading: false,
        errors: {}
    };

    return shallow(<FileForm {...props}/>);

}

describe('FileForm component tests', () => {


it('FileForm renders form',() => {
    const wrapper = setup({});
    expect(wrapper.find('form').length).toBe(1);
});

it('FileForm renders two submit button',() => {
    const wrapper = setup({});
    expect(wrapper.find('input').length).toBe(2);
});

});




