using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;

namespace VRS.UI
{
	internal class ViewStyleSerializer : CodeDomSerializer 
	{
		#region override Deserialize

		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			// This is how we associate the component with the serializer.
			CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(ViewStyle).BaseType, typeof(CodeDomSerializer));
			return baseClassSerializer.Deserialize(manager, codeObject);
        }

		#endregion
 
        public override object Serialize(IDesignerSerializationManager manager, object value) 
		{ 
			ViewStyle viewStyle = (ViewStyle)value;

			CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(value.GetType().BaseType,typeof(CodeDomSerializer));
			object codeObject = baseClassSerializer.Serialize(manager, value);
            if(codeObject is CodeStatementCollection){
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;

				ArrayList dontSerializeList = new ArrayList();
 			
				//--- loop through all statements
				foreach(CodeStatement st in statements){
					if(st is CodeAssignStatement){
						CodeAssignStatement cAssign = (CodeAssignStatement)st;
                        
						// If left is eg. 'this.ViewStyle.BorderColor'
						if(cAssign.Left is CodePropertyReferenceExpression){
							string propertyName  = ((CodePropertyReferenceExpression)cAssign.Left).PropertyName;
							object propertyValue = null;

							// If right is eg. 'System.Drawing.Color.FromArgb(183,193,214)'
							if(cAssign.Right is CodeMethodInvokeExpression){
								CodeMethodInvokeExpression mInvokeExp = (CodeMethodInvokeExpression)cAssign.Right;

								if(mInvokeExp.Method.MethodName == "FromArgb"){
									CodeCastExpression cCastExpR = (CodeCastExpression)mInvokeExp.Parameters[0];
									CodeCastExpression cCastExpG = (CodeCastExpression)mInvokeExp.Parameters[1];
									CodeCastExpression cCastExpB = (CodeCastExpression)mInvokeExp.Parameters[2];

									int r = Convert.ToInt32(((CodePrimitiveExpression)cCastExpR.Expression).Value);
									int g = Convert.ToInt32(((CodePrimitiveExpression)cCastExpG.Expression).Value);
									int b = Convert.ToInt32(((CodePrimitiveExpression)cCastExpB.Expression).Value);
									
									propertyValue = Color.FromArgb(r,g,b);
								}
							}

							// If right is eg. 'System.Drawing.Color.Lime'
							if(cAssign.Right is CodePropertyReferenceExpression){
								CodePropertyReferenceExpression propRefExp = (CodePropertyReferenceExpression)cAssign.Right;
								CodeTypeReferenceExpression tRefExp = (CodeTypeReferenceExpression)propRefExp.TargetObject; 
								
								if(tRefExp.Type.BaseType == "System.Drawing.Color" || tRefExp.Type.BaseType == "System.Drawing.SystemColors"){
									propertyValue = Color.FromName(propRefExp.PropertyName);
								}
							}

							if(cAssign.Right is CodeFieldReferenceExpression){
								CodeFieldReferenceExpression fRefExp = (CodeFieldReferenceExpression)cAssign.Right;
							
								if(fRefExp.FieldName == "FullSelect"){
									propertyValue = VRS.UI.Controls.WOutlookBar.ItemsStyle.FullSelect;
								}
								if(fRefExp.FieldName == "IconSelect"){
									propertyValue = VRS.UI.Controls.WOutlookBar.ItemsStyle.IconSelect;
								}
								if(fRefExp.FieldName == "UseDefault"){
									propertyValue = VRS.UI.Controls.WOutlookBar.ItemsStyle.UseDefault;
								}
							}
							
							//--- Check if we need to serialize property.
							if(!viewStyle.MustSerialize(propertyName,propertyValue)){
								// Add to remove list
								dontSerializeList.Add(st);
							}
						}
					}
				}	
		
				// Remove not neede properties
				foreach(CodeStatement obj in dontSerializeList){
					statements.Remove(obj);
				}
            }
         
			return codeObject;
        }		
    }
}
