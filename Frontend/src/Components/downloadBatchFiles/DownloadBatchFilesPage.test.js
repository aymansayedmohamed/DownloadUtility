/*import expect from 'expect';
import React from 'react';
import{mount,shallow} from 'enzyme';
import {DownloadBatchFilesPage} from './downloadBatchFilesPage';

function setup(batchSources){

    const props = {
        batchSources: batchSources,
        actions: {downloadBatchFiles: (batchSources) => {return Promise.resolve();}}
    };
    return mount(<DownloadBatchFilesPage {...props} />);

}

describe('DownloadBatchFilesPage component tests', () => {



it('set error message when trying to save download empty sources',() => {
    const wrapper = setup();
    const downloadButton = wrapper.find('input').last();
    expect(downloadButton.prop('type')).toBe('submit');
    downloadButton.simulate('click');
    expect(wrapper.state().errors.title).toBe('Sources is required');
});



});




*/