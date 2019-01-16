/* Category section */
import React from 'react';

export class Category extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div className="four wide column">
                    <h3>Category</h3>
                    <div className="tooltip">Choose a category that fits your services.</div>
                </div>
                <div className="ten wide column">
                    <div className="fields">
                        <div className="five wide field">
                            <select className="ui fluid dropdown">
                                <option value="1">Graphics &amp; Design</option>
                                <option value="2">Digital Marketing</option>
                                <option value="3">Writing &amp; Translation</option>
                                <option value="4">Video &amp; Animation</option>
                                <option value="5">Music &amp; Audio</option>
                                <option value="6">Programming &amp; Tech</option>
                            </select>
                        </div>
                        <div className="five wide field">
                            <select className="ui fluid dropdown">
                                <option value="1">Web programming</option>
                                <option value="2">Word press</option>
                                <option value="3">Mobile apps &amp; web</option>
                                <option value="4">Databases</option>
                            </select>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}
