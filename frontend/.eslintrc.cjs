module.exports = {
  extends: ['next/core-web-vitals', 'plugin:@typescript-eslint/recommended'],
  rules: {
    'semi': 'off',
    '@typescript-eslint/semi': ['error', 'always'],
    '@typescript-eslint/member-delimiter-style': 'error',
    'max-len': [
      'warn', {
        code: 160
      }
    ],
    'function-paren-newline': ['error', 'never'],
    allowAllPropertiesOnSameLine: 0,
    quotes: ['error', 'single', { 'avoidEscape': true, 'allowTemplateLiterals': true}],
    'comma-dangle': ['error', 'never'],
    'space-before-function-paren': ['error', 'always'],
    'indent': ['error', 2],
    'no-multiple-empty-lines': ['error', {'max':1}],
    'keyword-spacing': 'error',
    'space-before-blocks': 'error',
    'no-empty': 'warn'
  }
};
