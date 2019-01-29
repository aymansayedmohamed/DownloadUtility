import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import TestUtils from 'react-addons-test-utils';
import AboutPage from './AboutPage';

function setup(file){

    return shallow(<AboutPage />);

}

describe('AboutPage component tests', () => {



it('AboutPage renders correct title data',() => {
    const wrapper = setup();
    expect(wrapper.find('h1').get(0).props.children).toEqual('About');
});



});




