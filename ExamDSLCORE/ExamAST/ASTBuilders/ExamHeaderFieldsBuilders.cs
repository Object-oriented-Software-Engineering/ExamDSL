﻿using ExamDSLCORE.ExamAST.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDSLCORE.ExamAST.ASTBuilders {
    public class ExamHeaderTitleBuilder : BaseBuilder {
        public ExamHeaderTitle M_Product { get; init; }
        
        public ExamHeaderTitleBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderTitle(M_FormattingContext);
        }

        public TextBuilder<ExamHeaderTitleBuilder> Text() {
            TextBuilder< ExamHeaderTitleBuilder> newTextBuilder = new TextBuilder<ExamHeaderTitleBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderSemesterBuilder : BaseBuilder {
        public ExamHeaderSemester M_Product { get; init; }

        public ExamHeaderSemesterBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderSemester(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderSemesterBuilder> Text() {
            TextBuilder<ExamHeaderSemesterBuilder> newTextBuilder = new TextBuilder<ExamHeaderSemesterBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }

        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderDateBuilder : BaseBuilder {
        public ExamHeaderDate M_Product { get; init; }

        public ExamHeaderDateBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderDate(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderDateBuilder> Text() {
            TextBuilder<ExamHeaderDateBuilder> newTextBuilder = new TextBuilder<ExamHeaderDateBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderDurationBuilder :BaseBuilder{
        public ExamHeaderDuration M_Product { get; init; }

        public ExamHeaderDurationBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderDuration(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderDurationBuilder> Text() {
            TextBuilder<ExamHeaderDurationBuilder> newTextBuilder = new TextBuilder<ExamHeaderDurationBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderTeacherBuilder : BaseBuilder {
        public ExamHeaderTeacher M_Product { get; init; }

        public ExamHeaderTeacherBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderTeacher(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderTeacherBuilder> Text() {
            TextBuilder<ExamHeaderTeacherBuilder> newTextBuilder = new TextBuilder<ExamHeaderTeacherBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderStudentBuilder : BaseBuilder {
        public ExamHeaderStudentName M_Product { get; }
        
        public ExamHeaderStudentBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.GetFormattingProperty(typeof(NewLineProperty))  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderStudentName(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderStudentBuilder> Text() {
            TextBuilder<ExamHeaderStudentBuilder> newTextBuilder = new TextBuilder<ExamHeaderStudentBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }

    public class ExamHeaderDepartmentBuilder : BaseBuilder {
        public ExamHeaderDepartment M_Product { get; }

        public ExamHeaderDepartmentBuilder(ExamHeaderBuilder parent) {
            // 1. Initialize Formatting context
            M_FormattingContext = new TextFormattingContext() {
                M_Context = new Dictionary<Type, object>() {
                    { typeof(ScopeProperty), null },
                    { typeof(NewLineProperty),parent.M_FormattingContext.M_NewLineProperty  },
                    { typeof(OrderedItemListProperty), null}
                }
            };
            // 2. Initialize parent
            M_Parent = parent;
            // 3. Initialize product
            M_Product = new ExamHeaderDepartment(M_FormattingContext);
        }
        public TextBuilder<ExamHeaderDepartmentBuilder> Text() {
            TextBuilder<ExamHeaderDepartmentBuilder> newTextBuilder = new TextBuilder<ExamHeaderDepartmentBuilder>(this);
            M_Product.AddNode(newTextBuilder.M_Product, ExamHeader.TITLE);
            return newTextBuilder;
        }
        public ExamHeaderBuilder End() {
            return M_Parent as ExamHeaderBuilder;
        }
    }
}