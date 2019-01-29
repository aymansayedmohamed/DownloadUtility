import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import ViewWebPage from './ViewWebPage';

function setup(src){
    let props ={
        src: src
    };

    return shallow(<ViewWebPage {...props}/>);

}

describe('ViewWebPage component tests', () => {

it('ViewWebPage renders iframe',() => {
    const wrapper = setup("");
    expect(wrapper.find('iframe').length).toBe(1);
});

});








