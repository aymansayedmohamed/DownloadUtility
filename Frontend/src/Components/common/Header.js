import React, {PropTypes} from 'react';
import {Link, IndexLink } from 'react-router';

const Header = () => {
    return(
        <nav>
            <IndexLink to="/" activeClassName="active">Home</IndexLink>
            {" | "}
            <Link to="/about" activeClassName="active">About</Link>
            {" | "}
            <Link to="/readyForProcessingFiles" activeClassName="active">Ready For Processing Files</Link>
            {" | "}
            <Link to="/downloadBatchFiles" activeClassName="active">download Batch Files</Link>
        </nav>
    );
};

export default Header;