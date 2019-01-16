/* Seller Rating */
import React from 'react';

export default class SellerRating extends React.Component {
    render() {
        return (

            <div className="ten wide column seller-rating">
                <div className="ui grid">
                    <div className="five wide column">
                        <h3>Seller communication</h3>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                    </div>
                    <div className="five wide column">
                        <h3>Service as described</h3>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                    </div>
                    <div className="five wide column">
                        <h3>Would recommend</h3>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                        <i className="star yellow icon"></i>
                    </div>
                </div>
            </div>
            )
    }
}