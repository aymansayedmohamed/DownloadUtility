import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import HomePage from './HomePage';

function setup(file){

    return shallow(<HomePage />);

}

describe('HomePage component tests', () => {



it('HomePage renders correct title data',() => {
    const wrapper = setup();
    expect(wrapper.find('h1').get(0).props.children).toEqual('Download Utility');
});



});




