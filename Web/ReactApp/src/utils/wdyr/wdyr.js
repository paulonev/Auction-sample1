import React from 'react';

// why-did-you-render react: https://github.com/welldone-software/why-did-you-render/
if (process.env.NODE_ENV === 'development') {
  const whyDidYouRender = require('@welldone-software/why-did-you-render');
  whyDidYouRender(React, {
    onlyLogs: true,
    titleColor: "green",
    diffNameColor: "darkturquoise"
  });
}