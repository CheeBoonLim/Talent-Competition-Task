/* Service Detail Section */
import React from 'react';

export default class ServiceSectionDetail extends React.Component {
    render() {
        return (
            <div className="ten wide column">
                <div className="ui fluid card">
                    <div className="content service-description">
                        <div className="header">
                            <h4>Service</h4>
                        </div>
                    </div>
                    <div className="content">
                        <div className="ui grid">
                            <div className="eleven wide column">
                                <div className="gig-main-desc js-gig-main-desc has-border">
                                    <p>I've been working as a professional graphics designer for over XX years.</p>
                                    <p>I can paint beautiful paintings of flowers, portraits, etc.</p><br />
                                    <p><strong>Previous works</strong></p>
                                    <p><a href="https://google.com">https://example.com</a></p>
                                    <br />
                                    <p><strong>Package Include</strong></p>
                                    <ul>
                                        <li>&nbsp;A3 size 300Dpi poster</li>
                                        <li>&nbsp;High Quality Graphics </li>
                                        <li>&nbsp;3D mock up</li>
                                        <li>&nbsp;Fast delivery</li>
                                        <li>&nbsp;Stock images</li>
                                    </ul>
                                    <p>If you have any quotations, I'll be happy to help</p>
                                </div>
                            </div>

                            <div className="five wide column">

                                <h5>Format Type</h5>
                                <ul>
                                    <li>Posters</li>
                                </ul>
                                <h5>Illustration Style</h5>
                                <ul>
                                    <li>Photographic</li>
                                </ul>
                                <h5>Image File Format</h5>
                                <ul>
                                    <li>AI</li>
                                    <li>JPG</li>
                                    <li>PDF</li>
                                    <li>PNG</li>
                                    <li>PSD</li>
                                </ul>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            
            )
    }

}