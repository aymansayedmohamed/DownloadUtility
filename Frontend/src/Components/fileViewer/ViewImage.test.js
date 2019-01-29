import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import ViewImage from './ViewImage';

function setup(src){
    let props ={
        src: src
    };

    return shallow(<ViewImage {...props}/>);

}

describe('ViewImage component tests', () => {

it('ViewImage renders image',() => {
    const wrapper = setup("");
    expect(wrapper.find('img').length).toBe(1);
});

});








