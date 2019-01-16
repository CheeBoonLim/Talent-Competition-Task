/* Product Detail Section */
import React from 'react';

export default class ProductDetail extends React.Component {
    render() {
        return (
            <div className="ten wide column">
                <div className="ui fluid card">
                    <div className="content">
                        <h1><span className="skill-title">I paint beautiful pictures</span></h1>
                        <span className="meta">
                            <i className="star yellow icon"></i>
                            <i className="star yellow icon"></i>
                            <i className="star yellow icon"></i>
                            <i className="star yellow icon"></i>
                            <a href="#reviews" className="gig-ratings-count">(82)</a>
                        </span>

                        <span className="meta">4 Orders in Queue</span>

                        <div className="ui divider"></div>

                        <div className="header-bottom cf">
                            <div className="ui breadcrumb">
                                <a className="section">Graphics &amp; Design</a>
                                <i className="right angle icon divider"></i>
                                <a className="section">Brochures</a>
                                <i className="right angle icon divider"></i>
                                <div className="active section">Poster</div>
                            </div>
                        </div>
                    </div>
                    <div className="image">
                        <img src="/images/evie-shaffer-501641-unsplash.jpg"></img>
                    </div>
                    </div>
                </div>
                )
    }
}
