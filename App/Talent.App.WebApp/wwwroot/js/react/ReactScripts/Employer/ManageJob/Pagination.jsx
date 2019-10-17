import React from 'react';

export class Pagination extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="ui center aligned container" style={{ margin: "20px" }}>
                <div aria-label="Pagination Navigation" role="navigation" className="ui pagination menu ">
                    <a
                        aria-current="false"
                        aria-disabled="false"
                        tabIndex="0"
                        value="1"
                        aria-label="First item"
                        type="firstItem"
                        className="item"
                    >
                        <i aria-hidden="true" className="angle double left icon"></i>
                    </a>
                    <a
                        aria-current="false"
                        aria-disabled="false"
                        tabIndex="0"
                        value="4"
                        aria-label="Previous item"
                        type="prevItem"
                        className="item"
                    >
                        <i aria-hidden="true" className="angle left icon"></i>
                    </a>
                    <a aria-current="false" aria-disabled="false" tabIndex="0" value="1" type="pageItem" className="item">
                        1
                        </a>
                    <a
                        aria-current="false"
                        aria-disabled="false"
                        tabIndex="0"
                        value="6"
                        aria-label="Next item"
                        type="nextItem"
                        className="item"
                    >
                        <i aria-hidden="true" className="angle right icon"></i>
                    </a>
                    <a
                        aria-current="false"
                        aria-disabled="false"
                        tabIndex="0"
                        value="10"
                        aria-label="Last item"
                        type="lastItem"
                        className="item"
                    >
                        <i aria-hidden="true" className="angle double right icon"></i>
                    </a>
                </div>
            </div>
        )
    }
}