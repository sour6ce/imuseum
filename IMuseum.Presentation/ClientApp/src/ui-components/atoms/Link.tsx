import classNames from 'classnames';
import React from 'react';
import { Props } from '../../types/Props';
import { Link as RLink, LinkProps as RLinkProps } from 'react-router-dom'

export interface LinkProps extends Props,RLinkProps {
  target?: React.HTMLAttributeAnchorTarget;
}

export const Link: React.FC<LinkProps> = (props) => {
  return (
    <RLink
      to={props.to.toString()}
      className={classNames(props.className)}
      target={props.target}
    >
      {props.children}
    </RLink>
  );
};

export default Link;
