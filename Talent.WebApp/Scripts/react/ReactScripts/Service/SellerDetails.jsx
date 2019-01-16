/* Seller Detail Section */
import React from 'react';

export default class SellerDetail extends React.Component {
    render() {
        return (
            <div className="five wide column">
                <div className="ui fluid card">
                    <div className="content">
                        <div className="user-info">
                            <div className="ui small circular image">
                                <img src="/images/evie-shaffer-501641-unsplash.jpg"></img>
                              
                    </div>
                                <h3>Username</h3>
                            <div className="row-padded">
                                <i className="star yellow icon"></i>
                                <i className="star yellow icon"></i>
                                <i className="star yellow icon"></i>
                                <i className="star yellow icon"></i>
                                    <strong>4.7</strong><label>(1k+ reviews)</label>
                                </div>
                            <div className="row-padded">
                                <div className="ui teal button">Contact me</div>
                                </div>
                            </div>
                        <div className="ui list">
                            <div className="item">
                                <div className="right floated content">
                                        <strong>Sri Lanka</strong>
                                    </div>
                                <span><i className="large globe icon"></i> From</span>
                                </div>
                            <div className="item">
                                <div className="right floated content">
                                        <strong>June 2016</strong>
                                    </div>
                                    <span><i className="large user circle icon"></i> Member since</span>
                                </div>
                            <div className="item">
                                <div className="right floated content">
                                        <strong>3 hours</strong>
                                    </div>
                                <span><i className="large clock outline icon"></i> Avg response time</span>
                                </div>
                            <div className="item">
                                <div className="right floated content">
                                        <strong>9 hours ago</strong>
                                    </div>
                                <span><i className="large rocket icon"></i> Recent delivery</span>
                                </div>
                            </div>
                        <div className="ui divider"></div>
                            <div>
                                <p>I've been working as a professional graphics designer for over X years. I've worked on various design jobs from identity design to </p>
                                <a href="#">+ See More</a>
                            </div>
                        </div>
                    </div>
                </div>
            )
    }

}