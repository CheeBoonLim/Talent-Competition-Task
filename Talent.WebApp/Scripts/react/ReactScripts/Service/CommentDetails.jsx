/* Comments on Product*/
import React from 'react';

export default class CommentDetails extends React.Component {
    render() {
        return (
            <section className="ten wide column">
                <div id="reviews" className="ui comments">
                    <h3 className="ui dividing header">Comments</h3>
                    <div className="comment">
                        <a className="avatar">
                            <img src="/images/avatar/small/matt.jpg"/>
                      </a>
                            <div className="content">
                                <a className="author">Matt</a>
                                <div className="metadata">
                                    <span className="date">Today at 5:42PM</span>
                                </div>
                                <div className="text">
                                    How artistic!
                          </div>
                                <div className="actions">
                                    <a className="reply">Reply</a>
                                </div>
                            </div>
                  </div>
                        <div className="comment">
                            <a className="avatar">
                                <img src="/images/avatar/small/elliot.jpg"/>
                      </a>
                                <div className="content">
                                    <a className="author">Elliot Fu</a>
                                    <div className="metadata">
                                        <span className="date">Yesterday at 12:30AM</span>
                                    </div>
                                    <div className="text">
                                        <p>Dude, this is awesome. Thanks so much</p>
                                    </div>
                                    <div className="actions">
                                        <a className="reply">Reply</a>
                                    </div>
                                </div>
                                <div className="comments">
                                    <div className="comment">
                                        <a className="avatar">
                                            <img src="/images/avatar/small/jenny.jpg"/>
                              </a>
                                            <div className="content">
                                                <a className="author">Jenny Hess</a>
                                                <div className="metadata">
                                                    <span className="date">Just now</span>
                                                </div>
                                                <div className="text">
                                                    No problem :)
                                  </div>
                                                <div className="actions">
                                                    <a className="reply">Reply</a>
                                                </div>
                                            </div>
                          </div>
                                    </div>
                                </div>
                                <form className="ui reply form">
                                    <div className="field">
                                        <textarea></textarea>
                                    </div>
                                    <div className="ui teal labeled submit icon button">
                                        <i className="icon edit"></i> Comment
                      </div>
                                </form>
              </div>
          </section>
         )
    }
}
